import { Search } from 'lucide-react'
import React, { useState } from 'react'
import { useNavigate, useSearchParams } from 'react-router'

const SearchHeader = () => {
    const [keyworld, setkeyWorld] = useState<string>('')
    const navigate = useNavigate();

    const handleSearch = () => {
        if(!keyworld.trim()) return;
        navigate(`/search?q=${keyworld}`);
        setkeyWorld('')
    }
    return (
        <div className='relative flex-1'>
            <input
                type='text'
                value={keyworld}
                onChange={value => setkeyWorld(value.target.value)}
                onKeyDown={(e) => e.key === 'Enter' && handleSearch()}
                placeholder='Search...'
                className='w-full pl-10 p-2 rounded-md border focus:outline-none focus:border-2 hover:border-amber-200'
            />
            <Search 
                className='absolute top-2 right-4'
                onClick={handleSearch}
            />
        </div>
    )
}

export default SearchHeader