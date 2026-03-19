import * as z from 'zod';

export type Vouchers = {
    message: string,
    data: VoucherResponse[]
}
export type VoucherResponse = {
    id: number,
    code: string,
    discountType: number,
    value: number,
    minOrder: number,
    maxUsage: number,
    expiryDate: string
}

export const schema = z.object({
    discountType: z.number(),
    value: z.number(),
    minOrder: z.number(),
    maxUsage: z.number(),
    expiryDate: z.any()
})

export type VoucherCreate = z.infer<typeof schema>;
export const DiscountType = [
    {lable: 'Percent', value: 1},
    {lable: 'Fixed', value: 2}
]