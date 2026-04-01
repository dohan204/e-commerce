import { MapPin, Mail, Facebook, Phone } from "lucide-react";

const InformationContact = () => {
    return (
        <div className="flex flex-1 items-center justify-center p-6">
            <div className="w-full max-w-lg bg-white border shadow-xl rounded-xl p-6">
                
                <h3 className="text-xl font-bold text-center mb-6">
                    Thông tin liên hệ
                </h3>

                <div className="flex flex-col gap-4 text-gray-700">

                    {/* address */}
                    <div className="flex items-center gap-3">
                        <MapPin className="text-blue-500" />
                        <span>Nghĩa lộ, Yên Nghĩa, Hà Đông</span>
                    </div>

                    {/* email */}
                    <div className="flex items-center gap-3">
                        <Mail className="text-blue-500" />
                        <a 
                            href="mailto:dohan20055@gmail.com"
                            className="hover:text-blue-500"
                        >
                            dohan20055@gmail.com
                        </a>
                    </div>

                    {/* facebook */}
                    <div className="flex items-center gap-3">
                        <Facebook className="text-blue-500" />
                        <a 
                            href="https://www.facebook.com/doquochans"
                            target="_blank"
                            className="hover:text-blue-500"
                        >
                            Facebook cá nhân
                        </a>
                    </div>

                    {/* phone */}
                    <div className="flex items-center gap-3">
                        <Phone className="text-blue-500" />
                        <a 
                            href="tel:0382068238"
                            className="hover:text-blue-500"
                        >
                            0382 068 238
                        </a>
                    </div>

                </div>
            </div>
        </div>
    )
}

export default InformationContact