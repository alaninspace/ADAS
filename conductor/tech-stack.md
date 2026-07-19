# Technology Stack

## Core Orchestrator & Agents
- **Programming Language:** C# (.NET)
- **Agent Framework:** Microsoft Agent Framework
- **Orchestration Pattern:** Magentic Orchestration
- **Execution Strategy:** Agentless (WinRM/Remote PowerShell, SSH, SQL standard connections)

## Backend Infrastructure
- **Framework:** ASP.NET Core
- **Real-time Communication:** SignalR (WebSockets) for real-time streaming of agent plans, logs, and approval requests.
- **LLM Gateway:** Foundry API Gateway (OpenAI standards)

## Frontend UI
- **Framework:** Blazor WebAssembly
- **User Interface:** Terminal-inspired, dark mode web interface optimized for operational density.

## Database & Memory
- **Database System:** SQL Server 2025
- **Features Used:** Native Vector Support for persistent RAG (Retrieval-Augmented Generation) memory of past incidents and resolutions.
