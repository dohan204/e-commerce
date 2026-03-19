import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import type { TopProductsSales } from "@/models/products"


const TopSales = ({ data }: { data: TopProductsSales[] }) => {
    const dataConvert = data.sort((a, b) => b.sold - a.sold)
    return (
        <>
            <div>
                <Table>
                    <TableCaption>.</TableCaption>
                    <TableHeader>
                        <TableRow>
                            <TableHead>Mã sản phẩm</TableHead>
                            <TableHead>Tên sản phẩm</TableHead>
                            <TableHead>Giá bán</TableHead>
                            <TableHead>Đã bán</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {dataConvert.map((item) => (
                            <TableRow key={item.id}>
                                <TableCell>{item.id}</TableCell>
                                <TableCell>{item.name}</TableCell>
                                <TableCell>{item.price}</TableCell>
                                <TableCell>{item.sold}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>
        </>
    )
}

export default TopSales