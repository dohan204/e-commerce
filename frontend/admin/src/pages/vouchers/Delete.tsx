import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogMedia,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { Button } from "@/components/ui/button"
import { API_ENDPOINTS } from "@/constants/urls"
import type { VoucherResponse } from "@/models/vouchers"
import { authService } from "@/services/authService"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import { Trash2Icon } from "lucide-react"
import { useState, type ReactNode } from "react"
import { toast } from "sonner"

export default function Delete({ children, item }: { children: ReactNode, item: VoucherResponse }) {
  const [open, setOpen] = useState<boolean>(false);

  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async (): Promise<void> => {
      const res = await fetch(API_ENDPOINTS.VOUCHER.DELETE(item.id), {
        method: 'DELETE',
        headers: {
          "Authorization": `Bearer ${authService.getToken()}`
        }
      });

      if (!res.ok)
        throw new Error("error")
    }, onSuccess: () => {
      toast.success("Xóa thành công.", {position: 'top-center'})
      setOpen(false);
      queryClient.invalidateQueries({queryKey: ['vouchers']});
    },
    onError: (err) => {
      toast.error("Xóa Thất bại", { position: 'top-center'});
      console.log(err)
    }
  })
return (
  <AlertDialog open={open} onOpenChange={setOpen}>
    <AlertDialogTrigger asChild>
      <Button variant={'destructive'}>{children}</Button>
    </AlertDialogTrigger>
    <AlertDialogContent size="sm">
      <AlertDialogHeader>
        <AlertDialogMedia className="bg-destructive/10 text-destructive dark:bg-destructive/20 dark:text-destructive">
          <Trash2Icon />
        </AlertDialogMedia>
        <AlertDialogTitle>Xóa mã giảm giá: {item.code}</AlertDialogTitle>
        <AlertDialogDescription>
          Xóa mã giảm giá này chứ, nếu có hãy nhấn vào nút XÓA!.
        </AlertDialogDescription>
      </AlertDialogHeader>
      <AlertDialogFooter>
        <AlertDialogCancel variant="outline">Hủy</AlertDialogCancel>
        <AlertDialogAction onClick={() => mutation.mutate()} variant="destructive" disabled={mutation.isPending}>Xóa</AlertDialogAction>
      </AlertDialogFooter>
    </AlertDialogContent>
  </AlertDialog>
)
}
