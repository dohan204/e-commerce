import Footer from '@/components/Footer'
import Header from '@/components/Header'
import Main from '@/components/Main'
import Navigation from '@/components/Navigation'
import { TooltipProvider } from '@/components/ui/tooltip'
import React from 'react'
import { Toaster } from 'sonner'

const Layout = () => {
  return (
    <div className='flex flex-col'>
      <TooltipProvider>

        <Header />
        <div className='flex-1'>
          <Navigation />
          <div className='w-full'>
            <Main />
            <Toaster />
          </div>
          <Footer />
        </div>
      </TooltipProvider>
    </div>
  )
}

export default Layout