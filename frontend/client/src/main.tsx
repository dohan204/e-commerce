import { StrictMode } from "react"
import { createRoot } from "react-dom/client"

import "./index.css"
import App from "./App.tsx"
import { createBrowserRouter, RouterProvider } from "react-router"
import Home from "./pages/homes/index.tsx"
import Category from "./pages/categories/index.tsx"
import Promotion from "./pages/promotions/index.tsx"
import Contact from "./pages/contacts/index.tsx"
import DetailsProduct from "./pages/products/index.tsx"
import { UserProvider } from "./hooks/useUserContext.tsx"
import Login from "./pages/authentication/Login.tsx"
import Payments from "./pages/payments/index.tsx"
import Order from "./pages/orders/index.tsx"
import SearchResult from "./components/SearchResult.tsx"
import OrderHistory from "./pages/orders/OrderHistory.tsx"
import Notification from "./pages/notifications/index.tsx"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import OrderSuccess from "./pages/orders/OrderSuccess.tsx"

const query = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      staleTime: 2 * 1000 * 60,
    }
  }
});
const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {
        index: true,
        element: <Home />
      },
      {
        path: '/categories',
        element: <Category />,
        children: [
          {
            path: ':slug',
            element: <Category />
          }
        ]
      },
      {
        path: '/promotions',
        element: <Promotion />
      },
      {
        path: '/contact',
        element: <Contact />
      }, {
        path: '/product/detail/:id',
        element: <DetailsProduct />
      },
      {
        path: '/payments/:id',
        element: <Payments />
      },
      {
        path: '/order',
        element: <Order />
      },
      {
        path: '/search',
        element: <SearchResult />
      },
      {
        path: '/notifications',
        element: <Notification />
      },
      {
        path: '/orders',
        element: <OrderHistory />
      },
      {
        path: '/orders/success',
        element: <OrderSuccess />
      }
    ]
  },
  {
    path: '/login',
    element: <Login />
  }
])
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <QueryClientProvider client={query}>
        <UserProvider>
          <RouterProvider router={router} />
        </UserProvider>
    </QueryClientProvider>
  </StrictMode>
)
