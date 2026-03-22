# Copilot Instructions

## Project Guidelines
- Workspace projects target .NET 10 (reminder stored)
- User's workspace root is `C:\Users\Golam Khan\source\repos\Employee`
- Preferred terminal is `powershell.exe`

## Database Context Guidelines
- When updating DbContext, prefer using `DateTime.UtcNow` for audit timestamps and `Environment.UserName` for the current user in the absence of an injected user service.