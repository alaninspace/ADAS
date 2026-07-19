# Product Guidelines

## Brand & UI Identity
- **Visual Style:** Terminal-inspired, dark mode by default. The interface should feel like an advanced command-line tool, optimized for dense information display.
- **Layout:** Maximize screen real-estate for logs, metrics, and code blocks. Avoid unnecessary whitespace or overly bubbly chat bubbles.

## Voice & Tone
- **Professional & Direct:** The agents must communicate concisely. Present the facts, the relevant logs, and the executable code without conversational filler.
- **Authoritative but Safe:** The agents act as expert assistants but always defer to the human operator for final execution.

## UX Principles
- **Explicit Risk Communication:** Any proposed command that modifies system state (restarting services, killing queries, altering data) MUST be preceded by a strict, bold warning outlining the potential impact.
- **Clear Human-in-the-Loop (HITL) Handoffs:** When an agent requires approval, the UI must present the exact command to be executed (PowerShell, bash, SQL) in a clearly demarcated, read-only code block with unambiguous "Approve" or "Reject" actions.
- **Traceability:** Every action proposed by the agents must cite the evidence (e.g., the specific event log or SQL metric) that led to the recommendation.
