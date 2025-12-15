### Description

In this task you’ll build your first real React page. React is a component-based UI library for building user interfaces using small, reusable pieces called components. Use TypeScript for safety — it catches mistakes early and makes model/prop typing explicit.

Use arrow-function (function) components with hooks instead of class components. Function components are shorter, avoid `this` binding issues, and work naturally with hooks like `useState` and `useEffect`. Class components still work, but they’re verbose and unnecessary for this task.

You will:
- fetch all records from the backend using Axios
- store them in component state,
- render each record as a card,
- and configure Prettier so project formatting is consistent.

### Quick difference: Arrow (function) components vs Class components

- **Function components (arrow)**
    - Use plain functions/arrow functions that return JSX.
    - Use hooks (`useState`, `useEffect`) for state and lifecycle.
    - Less boilerplate, easier to test and read.
- **Class components**
    - Use `class` and `extends React.Component`.
    - Use `this.state`, lifecycle methods (`componentDidMount`, etc.).
    - More verbose and `this` can cause confusion.

Prefer arrow-function components in all new code.

---

### Example Hook Usage (TypeScript)

```ts
// BaseModelsPage.tsx
import React, { useEffect, useState } from "react";
import axios from "axios";
import { Card } from "./Card"; // assume a simple Card component
import type { MyModel } from "../types";

const BaseModelsPage: React.FC = () => {
  const [items, setItems] = useState<MyModel[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let mounted = true;
    axios
      .get<MyModel[]>("/api/models")
      .then((res) => {
        if (!mounted) return;
        setItems(res.data);
      })
      .catch((err) => {
        if (!mounted) return;
        setError(err?.message ?? "Failed to load");
      })
      .finally(() => {
        if (!mounted) return;
        setLoading(false);
      });

    return () => {
      mounted = false;
    };
  }, []);

  if (loading) return <div>Loading…</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <main>
      <h1>Base Models</h1>
      <section style={{ display: "grid", gap: 12 }}>
        {items.map((it) => (
          <Card key={it.id} model={it} />
        ))}
      </section>
    </main>
  );
};

export default BaseModelsPage;

```

---

### Steps to Complete

1. **Create the page component**
    - `BaseModelsPage.tsx` as a TypeScript arrow-function component.
    - Keep layout minimal: header + grid/list for cards.
2. **Model types**
    - Create a `types.ts` or `models.ts` and define `MyModel` to match backend DTOs.
3. **State and loading**
    - `useState<MyModel[]>` for data, plus small `loading` and `error` states.
4. **Fetch data in `useEffect`**
    - Use Axios inside `useEffect` (empty deps) to call `/api/models`.
    - Keep basic error handling and a mounted flag to avoid state updates on unmounted components.
5. **Render cards**
    - Map over `items` and render a simple `Card` component.
    - Each card should show the model’s main fields; keep styles lightweight.
6. **Prettier setup**
    - Install Prettier and add `.prettierrc` (example below).
    - Ensure editor formats on save (VS Code — enable format on save or use a pre-commit hook).
7. **Final cleanup**
    - Remove dead code, fix imports, and make sure there are no console errors or TS type errors.

---

### Sample `.prettierrc`

```json
{
  "semi": true,
  "singleQuote": true,
  "trailingComma": "all",
  "printWidth": 100,
  "tabWidth": 2,
  "bracketSpacing": true,
  "jsxSingleQuote": true,
  "plugins": ["prettier-plugin-organize-imports"]
}
```

---

### Topics to Learn

- What React is and component-based UI design
- Why prefer arrow-function components and how they differ from classes
- TypeScript basics for React (typing props, state, API responses)
- `useState` for local state
- `useEffect` for side effects and data fetching
- Axios for HTTP calls
- Rendering lists and basic accessible markup
- Project formatting with Prettier

---

### Notes / Tips (short)

- Type your API responses — don’t use `any`.
- Keep components small: `BaseModelsPage` (fetch + layout) → multiple small presentational `Card` components.
- Avoid inline heavy logic in JSX; move rendering helpers out if they grow.