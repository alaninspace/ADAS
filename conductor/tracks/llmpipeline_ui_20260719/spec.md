# Specification: LLM Pipeline Fix & Premium Chat UI Redesign

## 1. Overview
This track addresses two primary areas of the OpsWorkbench application:
1. **Backend:** Resolving connection and integration issues with the Foundry API Gateway and SignalR streaming in the LLM pipeline, enabling robust chat capabilities.
2. **Frontend:** Completely redesigning the Blazor WebAssembly chat UI to meet a premium modern standard. The design will draw inspiration from GitHub Copilot and Codex chat interfaces, featuring a terminal-inspired, dark mode aesthetic enriched with glassmorphism, smooth micro-animations, and high operational density.

## 2. Functional Requirements
- **LLM Pipeline (Backend):**
  - Establish a stable, reliable connection to the Foundry API Gateway for seamless LLM chat.
  - Implement robust error handling and retry mechanisms for gateway connections.
  - Fix SignalR streaming to ensure real-time, chunked delivery of LLM responses to the frontend.
- **Chat UI (Frontend):**
  - Implement a terminal-inspired dark mode theme inspired by GitHub Copilot/Codex chat UI.
  - Integrate a **Model Picker** to allow users to dynamically swap the underlying LLM model.
  - Integrate an **Agent Selector** to allow users to switch between specialized agents (GeneralAgent, DbaAgent, WindowsAdminAgent, LinuxAdminAgent).
  - Integrate glassmorphism effects (backdrop filters) for overlays, dropdowns (pickers), and floating elements.
  - Add smooth micro-animations for message appearance, agent typing indicators, and user interactions.

## 3. Non-Functional Requirements
- **Performance:** SignalR streaming must deliver tokens with minimal latency. The UI must render smoothly at 60fps during animations.
- **Aesthetics:** The UI must adhere to modern premium design standards, ensuring high readability and a polished developer experience.

## 4. Acceptance Criteria
- [ ] Backend successfully connects to the Foundry API Gateway and streams conversational responses via SignalR.
- [ ] Chat UI displays a dark mode, Copilot-inspired design with glassmorphism elements.
- [ ] Users can successfully change the active LLM using the Model Picker.
- [ ] Users can successfully select the target agent (General, DBA, Windows, Linux) via the Agent Selector.
- [ ] New messages and agent actions trigger smooth micro-animations.

## 5. Out of Scope
- Implementing the *actual logic* for the Windows/Linux/DBA agents beyond routing messages to them. This track focuses on the pipeline and UI scaffolding required to select and converse with them.
