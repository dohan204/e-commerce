import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import type { RatingProducts } from "@/models/products"
import React from 'react'

const Rating = ({data}: {data: RatingProducts[]}) => {
  
  const totalRating = data.reduce((cal, item) => cal + item.reviewCount, 0);
  const sortedData = [...data].sort((a, b) => b.avgRating - a.avgRating);
  console.log(totalRating);
  return (
    <div>
      <Table>
        <TableCaption>.</TableCaption>
        <TableHeader>
          <TableRow>
            <TableHead>Mã sản phẩm</TableHead>
            <TableHead>Tên sản phẩm</TableHead>
            <TableHead>Mô tả sản phẩm</TableHead>
            <TableHead>Đánh giá trung bình</TableHead>
            <TableHead>Tổng đánh giá</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {sortedData.map((item) => (
            <TableRow key={item.id}>
              <TableCell>{item.id}</TableCell>
              <TableCell>{item.name}</TableCell>
              <TableCell>{item.description}</TableCell>
              <TableCell>{item.avgRating}</TableCell>
              <TableCell>{item.reviewCount}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  )
}

export default Rating