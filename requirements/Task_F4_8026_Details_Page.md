### Description
You already have a list page that displays all base models as cards. The next step is creating a **details page** that shows full information about a selected item.

To support multiple pages, you’ll introduce **routing**. Routing in React lets you control which component is rendered based on the URL. For example:

- `/models` → list page  
- `/models/123` → details page for item with ID `123`

React Router is the standard solution for this. You define routes, link between them, and extract parameters (like the ID) from the URL when rendering the details page.

The goal of this task:
- Add routing to your project.
- Add a details page.
- Navigate from the list to the item details.
- Fetch the selected item using its ID.
- Render a clean details layout.

---

### What is Routing?
Routing maps URL paths to React components. It gives your app a “multi-page” feel while staying a single-page application under the hood.

With React Router, you typically:
- Wrap the app in `<BrowserRouter>`
- Define `<Routes>` and `<Route path="">`
- Use `<Link>` or `useNavigate` to move between pages
- Use `useParams` to extract route parameters like item IDs

Example concept (not the final code you’ll write):
```tsx
<Routes>
  <Route path="/models" element={<BaseModelsPage />} />
  <Route path="/models/:id" element={<ModelDetailsPage />} />
</Routes>
```

### Steps to Complete

1. **Install React Router**  
    Add `react-router-dom` to your project.
2. **Wrap your app with BrowserRouter**  
    Usually in `index.tsx` or `App.tsx`.
3. **Define routing config**
    - `/models` → list page
    - `/models/:id` → details page
4. **Create the details page component**
    - Arrow-function component in TypeScript (`ModelDetailsPage.tsx`).
    - Use `useParams` to extract the `id`.
    - Fetch the item details using Axios in `useEffect`.
5. **Add navigation from the list page**
    - Wrap each card with a `<Link>` or use a button that navigates using `useNavigate`.
    - Route should include the ID of the selected item.
6. **Render the details nicely**
    - Display all important properties of the model.
    - Don’t dump raw JSON — structure the UI.
7. **Keep formatting consistent**
    - Prettier should still auto-format everything.
8. **Small cleanup**
    - Remove unused imports.
    - Ensure no TypeScript errors.
    - Make sure routing works with refresh (BrowserRouter handles it).