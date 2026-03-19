import { StrictMode } from "react"
import { createRoot } from "react-dom/client"

import "./index.css"
import App from "./App.tsx"
import { createBrowserRouter, RouterProvider } from "react-router"
import Dashboard from "./pages/dashboards/index.tsx"
import Order from "./pages/orders/index.tsx"
import Category from "./pages/categories/index.tsx"
import User from "./pages/users/index.tsx"
import Products from "./pages/products/index.tsx"
import Voucher from "./pages/vouchers/index.tsx"
import Login from "./pages/Auth/index.tsx"
import Revenue from "./pages/Revenue/index.tsx"
import Report from "./pages/reports/index.tsx"

const router = createBrowserRouter([
  {
    path: '/login',
    element: <Login />,
  },
  {
    path: '/',
    element: <App />,
    children: [
      {
        index: true,
        element: <Dashboard />
      },
      {
        path: '/orders',
        element: <Order />
      },
      {
        path: "/categories",
        element: <Category />
      },
      {
        path: '/users',
        element: <User />
      },
      {
        path: '/products',
        element: <Products />
      },
      {
        path: '/vouchers',
        element: <Voucher />
      },
      {
        path: '/revenue',
        element: <Revenue />
      },
      {
        path: '/reports',
        element: <Report />
      }
    ]
  }
])
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <RouterProvider router={router} />
    {/* <App /> */}
    {/* </RouterProvider> */}
  </StrictMode>
)
