import React from 'react'

const Footer = () => {
  return (
    <footer className="w-full bg-gray-100 border-t mt-10">
      <div className="max-w-7xl mx-auto px-6 py-6 flex flex-col md:flex-row items-center justify-between gap-4">
        
        {/* Left */}
        <h5 className="text-gray-600 text-sm">
          © 2026 Admin Panel. All rights reserved.
        </h5>
        {/* Right */}
        <div className="flex gap-4 text-sm text-gray-500">
          <span className="cursor-pointer hover:text-black">Privacy</span>
          <span className="cursor-pointer hover:text-black">Terms</span>
          <span className="cursor-pointer hover:text-black">Contact</span>
        </div>

      </div>
    </footer>
  )
}

export default Footer