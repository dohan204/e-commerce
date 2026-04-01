import React, { useState } from 'react'

const FormContact = () => {
  const [form, setForm] = useState({
    email: '',
    phone: '',
    content: ''
  })

  const [error, setError] = useState<string | null>(null)

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value
    })
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()

    // validate đơn giản
    if (!form.email || !form.phone || !form.content) {
      setError("Vui lòng nhập đầy đủ thông tin")
      return
    }

    if (!form.email.includes("@")) {
      setError("Email không hợp lệ")
      return
    }

    setError(null)

    console.log("DATA:", form)

    // reset form
    setForm({
      email: '',
      phone: '',
      content: ''
    })
  }

  return (
    <div className='flex flex-1 items-center justify-center p-6'>
      <form 
        onSubmit={handleSubmit}
        className='w-full max-w-md bg-white border shadow-xl rounded-xl p-6 flex flex-col gap-4'
      >
        <h3 className='text-xl font-bold text-center'>Đăng ký phản hồi</h3>

        {/* email */}
        <input
          name="email"
          value={form.email}
          onChange={handleChange}
          placeholder='Enter email'
          className='border p-2 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-400'
        />

        {/* phone */}
        <input
          name="phone"
          value={form.phone}
          onChange={handleChange}
          placeholder='Enter phone'
          className='border p-2 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-400'
        />

        {/* content */}
        <textarea
          name="content"
          value={form.content}
          onChange={handleChange}
          placeholder='Enter content'
          className='border p-2 rounded-md min-h-[100px] focus:outline-none focus:ring-2 focus:ring-blue-400'
        />

        {/* error */}
        {error && (
          <p className='text-red-500 text-sm'>{error}</p>
        )}

        {/* button */}
        <button
          type="submit"
          className='bg-blue-500 text-white py-2 rounded-md hover:bg-blue-600 transition'
        >
          Gửi phản hồi
        </button>
      </form>
    </div>
  )
}

export default FormContact