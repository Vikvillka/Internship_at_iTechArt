### Description
In this task, you’ll add user authentication to the frontend.  
The backend already issues JWT and Refresh Tokens — now you need the UI flow to support logging in, storing tokens, and calling the backend with the proper headers.

You will introduce a **login modal**, a **global authentication state**, and a **Header/Footer layout**.  
The Header will contain a **Login** button that opens the modal. After successful authentication, store the tokens and refresh them when needed.

Use TypeScript everywhere, keep components small, and keep business logic inside containers or Redux logic (actions/sagas/selectors).

---

#### Steps to Complete

1. **Add Header and Footer**
   - Create two small reusable layout components.  
   - Header should contain your project name on the left and a Login button on the right.  
   - Footer can be simple — version number, your GitHub, or anything non-intrusive.

2. **Create Login Modal**
   - Clicking **Login** opens a modal.  
   - Modal contains username and password fields + a submit button.  
   - Use controlled inputs (React state).  
   - On submit → dispatch an authentication request action.

3. **Handle Authentication with Redux + Saga**
   - Add a new slice/module: `auth`.  
   - Introduce actions:  
     - `auth/login(credentials)`  
     - `auth/handleLoginSuccess({ token, refreshToken })`  
     - `auth/handleLoginFailure(error)`  
   - Saga flow:  
     1. On `loginRequest`, show loader.  
     2. Call the backend auth endpoint.  
     3. If successful — dispatch `loginSuccess`.  
     4. If failed — dispatch `loginFailure`.  
     5. Hide loader afterward.

4. **Store Tokens**
   - After `loginSuccess`, store tokens in Redux state.  
   - Also persist them in `localStorage` so they survive refresh.  
   - Rehydrate auth state from `localStorage` on app start.

5. **Attach Token to Backend Calls**
   - Add Axios interceptor.  
   - Read the token from Redux state.  
   - Attach `Authorization: Bearer <token>` header automatically.  
   - If backend returns 401 and you have a refresh token — trigger refresh flow (basic version only, no need to perfect it yet).

6. **Restrict Access to Protected Routes (Preview Only)**
   - Later tasks will expand this.  
   - For now, show an example:
```tsx
const isLoggedIn = useSelector(authSelectors.isLoggedIn);
if (!isLoggedIn) return <Navigate to="/" />;
```
   - Don’t implement full route guards yet — just understand the concept.

#### Example: Login Modal Component (View Only)

```tsx
export const LoginModal: React.FC<Props> = ({ isOpen, onClose, onSubmit }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  return (
    <Modal open={isOpen} onClose={onClose}>
      <div className="modal-body">
        <input
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="Username"
        />

        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="Password"
        />

        <button onClick={() => onSubmit({ username, password })}>Login</button>
      </div>
    </Modal>
  );
};
```

#### Example: Container for Login Modal (Logic)

```tsx
export const LoginModalContainer: React.FC = () => {
  const dispatch = useDispatch();

  const handleSubmit = (credentials: AuthCredentials) => {
    dispatch(authActions.login(credentials));
  };

  return (
    <LoginModal
      isOpen={useSelector(uiSelectors.isLoginModalOpen)}
      onClose={() => dispatch(uiActions.closeLoginModal())}
      onSubmit={handleSubmit}
    />
  );
};

```

### Topics to Learn

- JWT & Refresh Token flow
- Local token storage (`localStorage` vs Redux)
- Axios interceptors for authenticated requests
- Authentication request → saga → reducer → state update
- Controlled inputs for forms
- Managing modal windows in React
- Layout components (Header & Footer)
- Using TypeScript interfaces for auth models