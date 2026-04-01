import React from 'react'
import {
  Card,
  CardAction,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import OverViewDashboard from '@/components/OverViewDashboard';
import RevenueChart from './RevenueChart';
import ProductPie from './ProductPie';
const Dashboard = () => {
  return (
    <div>
      <OverViewDashboard />
      <div className='grid sm:grid-cols-1 md:grid-cols-2 gap-4 mt-6'>
        <RevenueChart />
        <ProductPie />
      </div>
    </div>
  )
}

export default Dashboard;