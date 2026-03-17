import { SidebarProvider, SidebarInset, SidebarTrigger } from "@/components/ui/sidebar"
import { AppSidebar } from "@/components/appp-sidebar"
import { Outlet } from "react-router"
import { Toaster } from "sonner"

export default function Layout() {
  return (
    <SidebarProvider>
      <div className="flex min-h-screen w-full bg-[#f8fafc]">
        {/* Cái này là cái bên trái */}
        <AppSidebar />

        {/* Cái này là phần nội dung chính bên phải */}
        <SidebarInset className="flex-1 flex flex-col overflow-hidden">
          {/* Header để chứa nút toggle */}
          <header className="flex h-16 items-center gap-4 border-b bg-white px-6">
            <SidebarTrigger />
            <h1 className="font-semibold text-slate-800">Dashboard / Categories</h1>
          </header>

          {/* Nội dung chính - Nơi nhét cái Table vào */}
          <main className="p-6 overflow-auto">
             {/* Gọi cái AdminTable của mày ở đây */}
             <Outlet />
             <Toaster />
          </main>
        </SidebarInset>
      </div>
    </SidebarProvider>
  )
}
