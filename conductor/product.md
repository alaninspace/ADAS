# OpsWorkbench

## Vision
OpsWorkbench is a centralized, open-source IT Operations assistant built on the Microsoft Agent Framework. It leverages a Magentic Orchestration pattern to provide a chat interface where specialized agents (Windows Admin, Linux Admin, and DBA) collaborate to investigate system issues, query databases, and propose fixes across on-premise and hybrid cloud environments. All remediation actions are gated by strict human-in-the-loop approvals, and past resolutions are indexed into an open-source vector database (like PostgreSQL with pgvector) to continuously improve diagnostic accuracy without requiring edge-host agent installations.

## Target Audience
Both Level 1/2 IT support staff (requiring guided, safe execution paths) and Senior Application Developers / DBAs (seeking to automate and speed up complex diagnostic and remediation tasks).

## Scope & Key Capabilities
- **Cross-Platform Diagnostics:** Seamlessly investigate issues across Windows Server infrastructure (Services, Event Logs, Disk I/O, IIS) and AWS Linux environments (including Docker containers).
- **Database Health:** Deep diagnostics for SQL Server performance and High Availability Groups, as well as PostgreSQL databases.
- **Strict Human-in-the-Loop (HITL):** Explicit human approval is required for any command that modifies system state, ensuring absolute control and safety.
- **Agentless Execution:** Execution relies on native administration protocols (WinRM/Remote PowerShell, SSH, SQL connections) orchestrated centrally.
