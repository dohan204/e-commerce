import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import type { ReactNode } from "react"

export interface ColumnConfig<T> {
    header: string,
    key: keyof T | string,
    className?: string,
    render?: (item: T) => ReactNode
}


interface DataTableProps<T> {
    data: T[],
    columns: ColumnConfig<T>[],
    caption?: string,
    footer?: ReactNode
}
export default function TableCustom<T>({data, columns, caption, footer}: DataTableProps<T>) {
  return (
    <div className="">
        <Table>
            {caption && <TableCaption>{caption}</TableCaption>}
            <TableHeader>
                <TableRow className="bg-muted/50">
                    {columns.map((col, index) => (
                        <TableHead key={index} className={col.className}>
                            {col.header}
                        </TableHead>
                    ))}
                </TableRow>
            </TableHeader>
            <TableBody>
          {data.length > 0 ? (
            data.map((item, rowIndex) => (
              <TableRow key={rowIndex}>
                {columns.map((col, colIndex) => (
                  <TableCell key={colIndex} className={col.className}>
                    {/* Nếu có hàm render riêng thì dùng, không thì hiện text thô */}
                    {col.render ? col.render(item) : (item[col.key as keyof T] as ReactNode)}
                  </TableCell>
                ))}
              </TableRow>
            ))
          ) : (
            <TableRow>
              <TableCell colSpan={columns.length} className="h-24 text-center">
                Không có dữ liệu.
              </TableCell>
            </TableRow>
          )}
        </TableBody>
        {footer && (
          <TableFooter>
            {footer}
          </TableFooter>
        )}
      </Table>
    </div>
  )
}