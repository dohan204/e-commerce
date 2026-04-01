import {create} from 'zustand';

interface CartState {
    isOpen: boolean,
    openCart: () => void,
    closeCart: () => void,
    toggleCart: () => void,
    refreshKey: number,
    triggerRefresh: () => void
}

export const useCartStore = create<CartState>((set) => ({
    isOpen: false,
    openCart: () => set({isOpen: true}),
    closeCart: () => set({isOpen: false}),
    toggleCart: () => set((state) => ({isOpen: !state.isOpen})),
    refreshKey: 0,
    triggerRefresh: () => set((state) => ({refreshKey: state.refreshKey + 1}))
}))