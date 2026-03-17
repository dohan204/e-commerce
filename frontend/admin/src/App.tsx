import { Button } from "@/components/ui/button"
import Layout from "./layout/layout"
import { authService } from "./services/authService"
import { useNavigate } from "react-router"

export function App() {
  const navigation = useNavigate();
  if(!authService.getToken) 
    navigation('/login')
  return (
      <Layout />
  )
}

export default App
