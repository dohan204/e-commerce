import { Card, CardContent } from '@/components/ui/card'
import View from './renderview'
import { API_ENDPOINTS } from '@/constants/urls'
import useFetchSingle from '@/hooks/use-fetchs'
import type { BaseResponse, Product, RatingProducts, TopProductsSales } from '@/models/products';
import TopSales from './topsales';
import Rating from './ratings';
import * as XLSX from 'xlsx';
import { Document, Packer, Paragraph, TextRun } from "docx";
import { saveAs } from "file-saver";
import { Button } from '@/components/ui/button';

const Report = () => {
    const test = [
        { ID: 1, Name: "Nguyễn Văn A", Email: "a@gmail.com", Role: "Admin" },
        { ID: 2, Name: "Trần Thị B", Email: "b@gmail.com", Role: "Editor" },
        { ID: 3, Name: "Lê Văn C", Email: "c@gmail.com", Role: "User" },
    ];
    const { data } = useFetchSingle<BaseResponse<Product>>(API_ENDPOINTS.PRODUCT.GETALL);
    if (data === null) return;

    const topSales: TopProductsSales[] = data?.data.map((item) => ({
        id: item.id,
        name: item.name,
        price: item.price,
        sold: item.sold
    })) ?? [];

    const ratings: RatingProducts[] = data?.data.map(item => ({
        id: item.id,
        name: item.name,
        description: item.description,
        avgRating: item.avgRating ? item.avgRating : 0,
        reviewCount: item.reviewCount ? item.reviewCount : 0
    }));

    const exportData = () => {
        // tạo một worksheet mới 
        const worksheet = XLSX.utils.json_to_sheet(test);

        // tạo một workbook mới (trang excel trống)
        const workbook = XLSX.utils.book_new();

        // thêm worksheet vào workbook và đặt tên là user
        XLSX.utils.book_append_sheet(workbook, worksheet, "user");

        // xuất file và tại vể máy 
        XLSX.writeFile(workbook, "DanhSachUser.xlsx");

    }

const exportWord =  async () => {
    const doc = new Document({
        sections: [
            {
                children: [
                    new Paragraph({
                        children: [
                            new TextRun("Hello world"),
                            new TextRun({
                                text: "Đây là file world từ react",
                                bold: true
                            })
                        ]
                    })
                ]
            }
        ]
    })

    const blob = await Packer.toBlob(doc);
    saveAs(blob, "file.docx");
}
    return (
        <div className='w-full'>
            <div className='h-16 bg-amber-50'>
                <h3 className='text-header'>Thống kê sản phẩm</h3>
            </div>
            <div>
                <View data={data} />
                <Button onClick={exportData}>export</Button>
                <Button onClick={exportWord}>export</Button>
            </div>
            <div className='mt-4'>
                <h4>Sản phẩm bán tốt nhất</h4>
                <TopSales data={topSales} />

            </div>
            <div className='mt-4'>
                <h4>Đánh giá sản phẩm</h4>
                <Rating data={ratings} />
            </div>
        </div>
    )
}

export default Report