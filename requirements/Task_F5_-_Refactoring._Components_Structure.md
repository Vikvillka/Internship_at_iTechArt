# Task 3 – Refactoring: Component Structure & Container/Presenter Split

### Description
At this point, your app works — but the code is probably starting to get messy. Pages are getting longer, responsibilities are mixed, and reusable UI pieces are hidden inside large files. Time to fix that.

This task focuses on **refactoring**, not adding new features. You’ll break down your existing pages into smaller, reusable components and separate **logic** from **presentation** using a container/presenter (a.k.a. smart/dumb components) pattern.

The goal:  
- Cleaner structure  
- Smaller files  
- Components with a single responsibility  
- Logic extracted out of visual components  
- Improved readability and maintainability  

---

### Why split components?
Any component longer than **~30–40 lines** usually mixes too much. When you hit that size:
- UI gets buried inside logic
- Reusable blocks remain trapped inside the page
- Testing becomes harder
- Bugs creep in due to state scattered everywhere

So the rule: **if a block of JSX or logic could be reused or understood in isolation — extract it.**

Examples to extract:
- A card UI  
- A details info section  
- A header/toolbar  
- A list wrapper  
- Any repeated block  
- Any logic that is unrelated to rendering  

---

### Container vs Presenter Components
This is the real cleanup. Your pages currently do both:
- Fetch data  
- Hold state  
- Render UI  
- Handle actions  
- Contain layout  

Split them into:
1. **Container component**  
   - Holds state  
   - Fetches data  
   - Handles events  
   - Prepares props  
   - Decides what presenter to render  

2. **Presenter (view) component**  
   - Receives data + handlers as props  
   - Renders UI only  
   - No Axios, no effects, no business logic  
   - Extremely predictable  

This makes components smaller, easier to test, and easier to reuse.

---

### Example Container/Presenter Split

#### Container (logic)
```tsx
// BaseModelsContainer.tsx
import { useEffect, useState } from "react";
import axios from "axios";
import type { MyModel } from "../types";
import BaseModelsView from "./BaseModelsView";

const BaseModelsContainer: React.FC = () => {
  const [items, setItems] = useState<MyModel[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get<MyModel[]>("/api/models").then(res => {
      setItems(res.data);
      setLoading(false);
    });
  }, []);

  const handleSelect = (id: string) => {
    // navigation logic goes here (useNavigate)
  };

  return (
    <BaseModels
      items={items}
      loading={loading}
      onSelect={handleSelect}
    />
  );
};

export default BaseModelsContainer;
```

#### Presenter (pure view)

```tsx
// BaseModelsView.tsx
import React from "react";
import { Card } from "../components/Card";
import type { MyModel } from "../types";

interface Props {
  items: MyModel[];
  loading: boolean;
  onSelect: (id: string) => void;
}

const BaseModelsView: React.FC<Props> = ({ items, loading, onSelect }) => {
  if (loading) return <div>Loading…</div>;

  return (
    <main>
      <h1>Base Models</h1>
      <section style={{ display: "grid", gap: 12 }}>
        {items.map(m => (
          <Card key={m.id} model={m} onClick={() => onSelect(m.id)} />
        ))}
      </section>
    </main>
  );
};

export default BaseModelsView;
```

**Notice:**
- The view knows nothing about Axios, routing, or state.
- The container knows nothing about how the UI looks.
- Communication happens through props (`items`, `loading`, `onSelect`).

### Steps to Complete

1. **Identify large components**
    - Find pages/components longer than ~40 lines.
    - Highlight UI blocks or logic blocks that can be extracted.
2. **Create container components**
    - Move all fetching, `useState`, `useEffect`, and event handlers into a container file.
    - Keep the container small and focused.
3. **Create presenter (pure UI) components**
    - Move JSX markup into separate components.
    - They should receive data + handlers as props.
4. **Extract reusable components**
    - Cards
    - Layout wrappers
    - Field sections for details pages
    - Buttons, headers, etc.
    - Anything that appears more than once should have its own file.
5. **Pass data and methods through props**
    - Containers → presenters
    - Containers → reusable components
    - Always typed using TypeScript interfaces.
6. **Simplify presenter components**
    - No logic
    - No Axios
    - No state (unless it’s UI-only, like a local toggle)
7. **Cleanup**
    - Remove leftover logic from view components
    - Fix imports after splitting
    - Use Prettier to normalize formatting

### Topics to Learn

- Component size limits and how to identify “extraction candidates”
- Single-responsibility principle applied to React components
- Container/presenter (smart/dumb) pattern
- Passing props efficiently (data + handlers)
- Creating reusable UI blocks
- Structuring pages into smaller files
- Keeping components stateless whenever possible

### Tips

- If a component starts mixing logic and JSX → split immediately.
- If your presenter component grows, extract UI chunks.
- Try to keep containers logic-heavy but small; avoid bloated god-components.
- Avoid drilling deeply nested props; if it gets bad, context will be your next lesson.