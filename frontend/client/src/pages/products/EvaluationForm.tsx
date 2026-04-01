import { Button } from '@/components/ui/button';
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { Spinner } from '@/components/ui/spinner';
import { API_ENDPOINTS } from '@/constants/UrlGlobal';
import { useRatingStore } from '@/store/useRatingStore';
import { useReviewStore } from '@/store/useReviewStore';
import { zodResolver } from '@hookform/resolvers/zod';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import { toast } from 'sonner';
import * as z from 'zod';


const schema = z.object({
  productEntityId: z.coerce.number().int(),
  ratings: z.coerce.number().min(1).max(5),
  comments: z.string().max(500).optional().or(z.literal('')),
})
const EvaluationForm = ({ isOpen, closeForm, id }: { isOpen: boolean, closeForm: () => void, id: number }) => {
  const [loading, setLoading] = useState(false);
  const {activeKey} = useReviewStore();
  const { register, handleSubmit } = useForm({
    resolver: zodResolver(schema),
    defaultValues: {
      productEntityId: id,
      ratings: 0,
      comments: '',
    }
  })

  const onSubmit = async (data: any) => {
    const token = localStorage.getItem('token');
    if(!token) return;
    setLoading(true)
    try {
      const res = await fetch(API_ENDPOINTS.Review.CREATE, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
      });

      if(!res) {
        throw new Error(`create faild`);
      }

      toast.success("Tạo đánh giá thành công", {position:'top-center'});
      activeKey();
      closeForm()
    } catch (err) {
      toast.error("Tạo thất bại", {position: 'top-center'});
      console.error(err)
    } finally {
      setLoading(false);
    }
  }
  return (
    <Dialog open={isOpen} onOpenChange={closeForm}>
      <DialogContent>
        <form onSubmit={handleSubmit(onSubmit)}>
          <DialogHeader>
            <DialogTitle>Đánh giá sản phẩm</DialogTitle>
            <DialogDescription>
              Tạo các đánh giá cho các sản phẩm giúp mọi người có cái nhìn tốt hơn về sản phẩm
            </DialogDescription>
          </DialogHeader>
          <input {...register('ratings')} type="number" min={1} max={5} placeholder="Đánh giá (1-5)" className="w-full mb-4 p-2 border rounded" />
          <textarea {...register('comments')} placeholder="Bình luận (tối đa 500 ký tự)" className="w-full mb-4 p-2 border rounded" maxLength={500} />
          <DialogFooter>
            <DialogClose asChild>
              <Button>Hủy</Button>
            </DialogClose>
            <Button type='submit' disabled={loading}>
              {loading ? (<div className='flex'>
                <Spinner /> <span>Đang tạo</span>
              </div>): <p>Tạo dánh giá</p>}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}

export default EvaluationForm