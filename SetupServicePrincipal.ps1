# SetupServicePrincipal.ps1
# This script creates or updates a service principal for GitHub Actions, assigns the required role, and outputs the credentials for use as GitHub secrets.

param(
    [string]$spName = "github-actions-deployer",
    [string]$resourceGroup = "acmetickets-rg",
    [string]$subscriptionId = "1a53289f-e11e-451f-9eae-fecbc5477478"
)

# Login to Azure interactively or with az login --service-principal ...
az account set --subscription $subscriptionId

# Get the service principal (by display name)
$sp = az ad sp list --display-name $spName | ConvertFrom-Json

if (-not $sp) {
    Write-Host "Creating service principal $spName..."
    $spObj = az ad sp create-for-rbac `
        --name $spName `
        --role "User Access Administrator" `
        --scopes "/subscriptions/$subscriptionId/resourceGroups/$resourceGroup" `
        --sdk-auth | ConvertFrom-Json
    $clientId = $spObj.clientId
    $clientSecret = $spObj.clientSecret
    $tenantId = $spObj.tenantId
    Write-Host "Service principal created."
    Start-Sleep -Seconds 10 # Give Azure AD time to propagate
} else {
    Write-Host "Service principal already exists. Fetching details..."
    $clientId = $sp[0].appId
    $tenantId = $sp[0].appOwnerOrganizationId
    # Reset the secret to get a new one
    $spObj = az ad sp credential reset --id $clientId --query "{clientId:appId, clientSecret:password, tenantId:tenant}" | ConvertFrom-Json
    $clientSecret = $spObj.clientSecret
    Write-Host "Service principal secret reset."
}

# Get the object id for role assignment
Write-Host "DEBUG: az ad sp show --id $clientId"
az ad sp show --id $clientId
$spObjectId = az ad sp show --id $clientId --query id -o tsv
if (-not $spObjectId -or $spObjectId -eq "") {
    Write-Host "ERROR: Could not determine the service principal object id for client id $clientId."
    exit 1
}
Write-Host "Service principal object id: $spObjectId"

# Assign User Access Administrator role at the resource group level
$rgScope = "/subscriptions/$subscriptionId/resourceGroups/$resourceGroup"
Write-Host "Assigning 'User Access Administrator' role to SP at scope $rgScope..."
az role assignment create `
    --assignee-object-id $spObjectId `
    --assignee-principal-type ServicePrincipal `
    --role "User Access Administrator" `
    --scope $rgScope | Out-Null

Write-Host "---"
Write-Host "Add the following as GitHub Actions secrets in your repository:"
Write-Host "AZURE_CLIENT_ID:      $clientId"
Write-Host "AZURE_CLIENT_SECRET:  $clientSecret"
Write-Host "AZURE_TENANT_ID:      $tenantId"
Write-Host "AZURE_SUBSCRIPTION_ID: $subscriptionId"
Write-Host "---"
Write-Host "Go to your GitHub repository > Settings > Secrets and variables > Actions, and add each value above as a separate secret."
