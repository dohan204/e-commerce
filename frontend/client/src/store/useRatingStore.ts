import { create } from 'zustand';
export interface RatingStore {
    isOpen: boolean;
    openForm: () => void;
    closeForm: () => void;
}

export const useRatingStore = create<RatingStore>((set) => ({
    isOpen: false,
    openForm: () => set({isOpen: true}),
    closeForm: () => set({isOpen: false}),
}))