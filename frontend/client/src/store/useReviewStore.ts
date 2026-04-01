import { create } from "zustand"

export interface ReviewStore {
    key: number,
    activeKey: () => void
}


export const useReviewStore = create<ReviewStore>((set) => ({
    key: 0,
    activeKey: () => set((state) => ({key: state.key + 1}))
}))