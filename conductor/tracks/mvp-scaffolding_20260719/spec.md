# Track: ADAS MVP Scaffolding

## Overview
This track focuses on the end-to-end scaffolding of the Application & Database Automated Support (ADAS) platform. It will establish the core Magentic Orchestrator, the SignalR backend, and the Blazor WebAssembly Chat UI. This initial MVP will prove the orchestration flow, real-time communication, and secure authentication (Entra ID OIDC) before building out complex edge-execution capabilities.

## Functional Requirements
- **Magentic Orchestrator:** Scaffold a basic Manager Agent and a single generic worker agent using the Microsoft Agent Framework to prove the orchestration flow.
- **Agent Initialization:** Use the provided Factory pattern (`OpenAIChatCompletionClient`) to instantiate agents, routing through the Fortescue AI Gateway.
- **Chat UI:** Build a Blazor WebAssembly frontend utilizing MudBlazor for a terminal-inspired, dark-mode interface.
- **Human-in-the-Loop (HITL):** Implement approval requests as standard chat messages with embedded action buttons (Approve/Reject) directly in the SignalR chat stream.
- **Authentication:** Implement Entra ID OIDC authentication using the Backend-for-Frontend (BFF) pattern (Cookie session + OIDC challenge), heavily relying on the provided `AdasAuthorization` constants and policies.
- **Configuration:** Implement robust configuration using `appsettings.json`, `appsettings.Development.json`, and `appsettings.Staging.json`. Use `secrets.json` for local development and environment variables for upper environments.

## Non-Functional Requirements
- **Namespace:** The root namespace for all components must be `Adas` and `Adas.Web`.
- **Observability:** Integrate OpenTelemetry (OTEL) as a first-class citizen, routing logs and metrics to the existing OpenObserve stack (`observe.fmg.local`).
- **Knowledge Format:** Prepare the infrastructure to consume the Open Knowledge Format (OKF) standard.
- **Infrastructure as Code:** Utilize YAML files for infrastructure definitions.

## Acceptance Criteria
- [ ] A developer can spin up the application locally using configuration mapped from `secrets.json` and `appsettings.Development.json`.
- [ ] A user is authenticated via Entra ID (OIDC) before accessing the Chat UI.
- [ ] The user can send a message via the MudBlazor UI, which is transmitted over SignalR to the backend.
- [ ] The Manager Agent receives the message, coordinates with the generic worker agent, and streams the response back to the UI.
- [ ] The UI successfully renders a mock HITL action button in the chat stream.
- [ ] Telemetry data is successfully exported to the OTEL endpoint.

## Out of Scope
- Implementation of the specialized `WindowsAdminAgent` and `DbaAgent` edge execution capabilities.
- Complex RAG / Vector Database ingestion (only configuration scaffolding is in scope).
