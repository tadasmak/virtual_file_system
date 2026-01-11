# Virtual File System

Console application for managing files in a virtual folder structure with persistence.

## Run
```bash
dotnet run --project Saldo_homework
```

## Test
```bash
cd Saldo_homework.Tests
dotnet test
```

## Commands

Type `help` in the application to see available commands.

## Example
```
> createdir /documents
> addfile C:\test.txt /documents
> tree
> exit
```

Data persists in `%APPDATA%\SaldoHomework\vfs.json`