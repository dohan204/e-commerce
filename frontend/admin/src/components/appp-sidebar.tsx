import { LayoutDashboard, Users, Package, Tags, Carrot, Notebook, NotebookIcon, View, FolderLockIcon } from "lucide-react"
import { Link, useLocation } from "react-router" // Thêm useLocation để check route

import { 
  Sidebar,  
  SidebarContent,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem, 
} from "./ui/sidebar"
import { iconLibraries } from "shadcn/icons";

const menuItems = [
  { title: "Dashboard", icon: LayoutDashboard, url: "/" },
  { title: "Categories", icon: Tags, url: "/categories" },
  { title: "Products", icon: Package, url: "/products" },
  { title: "Users", icon: Users, url: "/users" },
  { title: "Carts", icon: Carrot, url: "/carts"},
  { title: "Vouchers", icon: Notebook, url: '/vouchers'},
  { title: "Orders", icon: NotebookIcon, url: '/orders'},
  { title: "Review", icon: View, url: '/reviews'},
  { title: "Logout", icon: FolderLockIcon, url: '/login'}
]

export function AppSidebar() {
  const location = useLocation(); // Lấy URL hiện tại

  return (
    <Sidebar className="bg-[#0f172a] text-slate-300 border-none">
      <SidebarHeader className="p-6">
        <h2 className="text-xl font-bold text-blue-500">Admin Panel</h2>
      </SidebarHeader>
      
      <SidebarContent>
        <SidebarMenu className="px-2 gap-2">
          {menuItems.map((item) => {
            // Tự động kiểm tra: Nếu URL hiện tại khớp với item.url thì cho sáng lên
            const isActive = location.pathname === item.url;

            return (
              <SidebarMenuItem key={item.title}>
                <SidebarMenuButton 
                  asChild 
                  isActive={isActive}
                  className="data-[active=true]:bg-blue-600 data-[active=true]:text-white py-6 rounded-lg hover:bg-slate-800 transition-all cursor-pointer"
                >
                  <Link to={item.url}>
                    <item.icon className={isActive ? "text-white" : "text-blue-500"} />
                    <span className="font-medium">{item.title}</span>
                  </Link>
                </SidebarMenuButton>
              </SidebarMenuItem>
            );
          })}
        </SidebarMenu>
      </SidebarContent>
    </Sidebar>
  )
}
