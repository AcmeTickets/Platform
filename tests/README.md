# Platform Tests

This directory contains unit tests for the Platform solution.

## Test Projects

### PublicContracts.Tests
Tests for the public contracts library including:
- Contract property validation
- Serialization/deserialization tests
- Data integrity tests

### Api.Tests
Tests for the Platform API including:
- EventController endpoint tests
- Message sending validation
- API behavior verification

### Message.Tests
Tests for the Message handler service including:
- Message handler tests (to be expanded as handlers are implemented)

## Running Tests

To run all tests:
```bash
dotnet test
```

To run tests for a specific project:
```bash
dotnet test tests/PublicContracts.Tests/PublicContracts.Tests.csproj
dotnet test tests/Api.Tests/Api.Tests.csproj
dotnet test tests/Message.Tests/Message.Tests.csproj
```

To run tests with detailed output:
```bash
dotnet test --logger "console;verbosity=detailed"
```

To run tests with coverage:
```bash
dotnet test /p:CollectCoverage=true
```

## Test Framework

- **xUnit**: Test framework
- **Moq**: Mocking library for creating test doubles
- **FluentAssertions**: Fluent assertion library for more readable tests
- **NServiceBus.Testing**: Testing utilities for NServiceBus message handlers
