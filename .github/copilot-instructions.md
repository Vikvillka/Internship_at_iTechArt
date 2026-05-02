# Copilot Instructions

Project architecture is described in docs/CODEBASE_MAP.md.
Always refer to it when making structural changes.

---

## Task Context (GitHub Issues as MCP substitute)

This project uses GitHub Issues as the task tracker system.

Treat Issues as an MCP-like interface with 3 core capabilities:

### 1. Find Issues
Use repository issue search to locate relevant tasks by:
- title
- labels
- keywords
- status (open/closed)

### 2. Get Issue
When working on a task, always read the full GitHub Issue description before implementation.

The Issue is the source of truth for:
- requirements
- acceptance criteria
- constraints

### 3. Discussion Context
Use Issue comments as runtime context:
- clarifications
- decisions
- updates
- edge cases discovered during implementation

Always consider discussion history before making changes.
