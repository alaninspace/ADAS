# Implementation Plan

## Phase 1: Project Initialization & Configuration
- [x] Task: Scaffold ASP.NET Core Web API project (Adas.Api) and Blazor WebAssembly project (Adas.Web) 6ed8f12
  - [x] Initialize solution structure
  - [x] Setup `appsettings.json`, `appsettings.Development.json`, and `appsettings.Staging.json`
  - [x] Initialize `secrets.json` for development (specifically for LLM Gateway secrets)
- [~] Task: Integrate OpenTelemetry
  - [ ] Write Tests (OTEL configuration validation)
  - [ ] Implementation (Configure OTEL for logs and metrics pointing to `observe.fmg.local`)
- [ ] Task: Setup Open Knowledge Format (OKF) & Mock Infrastructure
  - [ ] Create basic YAML infrastructure definition for deployment
  - [ ] Define memory interfaces (e.g., `IMemoryStore`) and implement an in-memory Mock version for dev to bypass SQL Server requirements
- [ ] Task: Phase Verification & Checkpoint (Refer to workflow.md)

## Phase 2: Authentication (BFF & Entra ID)
- [ ] Task: Implement BFF OIDC Challenge
  - [ ] Write Tests (Auth endpoints return 401/redirects properly, and allow bypass in Dev)
  - [ ] Implementation (Wire in `AdasAuthorization` constants, Entra ID OpenIdConnect setup, with a dev-environment bypass flag)
- [ ] Task: Secure API Endpoints
  - [ ] Write Tests (Ensure API rejects unauthenticated requests unless auth is bypassed in Dev)
  - [ ] Implementation (Apply `OperatorPolicy` to all API endpoints, allowing mock identities/anonymous access in Dev mode if configured)
- [ ] Task: Phase Verification & Checkpoint (Refer to workflow.md)

## Phase 3: Magentic Orchestrator Backend
- [ ] Task: Implement LLM Gateway Client Factory
  - [ ] Write Tests (Client factory returns `OpenAIChatCompletionClient` with correct headers)
  - [ ] Implementation (Use Fortescue AI gateway config and wire the client secret from user secrets in Dev to hit real models)
- [ ] Task: Scaffold Manager Agent & Generic Worker
  - [ ] Write Tests (Agents can be instantiated and process a basic prompt using the real LLM endpoint)
  - [ ] Implementation (Configure Microsoft Agent Framework Magentic Manager and generic worker)
- [ ] Task: Implement SignalR Hub
  - [ ] Write Tests (SignalR connection accepts messages and streams responses)
  - [ ] Implementation (Create `ChatHub`, wire it to the Orchestrator, implement HITL mock message streaming)
- [ ] Task: Phase Verification & Checkpoint (Refer to workflow.md)

## Phase 4: Chat UI (Blazor)
- [ ] Task: Setup MudBlazor
  - [ ] Implementation (Install MudBlazor, configure Dark Theme)
- [ ] Task: Implement Chat Interface
  - [ ] Write Tests (Component rendering logic)
  - [ ] Implementation (Message list, input box, connect to SignalR backend)
- [ ] Task: Implement HITL Action Buttons
  - [ ] Write Tests (Action button click handlers)
  - [ ] Implementation (Render Approve/Reject buttons when HITL messages arrive from SignalR)
- [ ] Task: Phase Verification & Checkpoint (Refer to workflow.md)
