import { StrictMode } from "react"
import { createRoot } from "react-dom/client"

import "./index.css"
import App from "./App.tsx"
import { ThemeProvider } from "@/components/theme-provider.tsx"
import { createBrowserRouter, RouterProvider } from "react-router"
import Dashboard from "./pages/dashboards/index.tsx"
import Order from "./pages/orders/index.tsx"
import Category from "./pages/categories/index.tsx"
import User from "./pages/users/index.tsx"
import Product from "./pages/products/index.tsx"
import Voucher from "./pages/vouchers/index.tsx"
import Cart from "./pages/carts/index.tsx"
import Review from "./pages/reviews/index.tsx"
import Login from "./pages/Auth/index.tsx"
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
        element: <Product />
      },
      {
        path: '/vouchers',
        element: <Voucher />
      },
      {
        path: '/carts',
        element: <Cart />
      },
      {
        path: '/reviews',
        element: <Review />
      },

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
