import { Button } from '@/components/ui/button';

import { FacebookIcon } from '@/components/icons/facebook';
import { ChromeIcon } from '@/components/icons/chrome';
import { QrCodeIcon } from '@/components/icons/qrcode';
import { UserIcon } from '@/components/icons/user';

export default function Login() {
    const buttons = [
        {
            icon: QrCodeIcon,
            label: 'Use QR code',
            style: 'border border-gray-300 text-black',
        },
        {
            icon: UserIcon,
            label: 'Use phone / email / username',
            style: 'border border-gray-300 text-black',
        },
        {
            icon: FacebookIcon,
            label: 'Continue with Facebook',
            style: 'border border-gray-300 bg-[#1877f2] text-white',
        },
        {
            icon: ChromeIcon,
            label: 'Continue with Google',
            style: 'border border-gray-300 text-black',
        },
    ];

    return (
        <div className="w-112 rounded-md border-2 border-tiktok-red bg-white pb-8 pl-12 pr-12 pt-16">
            <h1 className="mb-6 p-4 text-center text-3xl font-bold text-custom-black">Log in to TikTok</h1>

            {buttons.map((button, index) => (
                <Button key={index} className={`mb-4 flex w-full items-center justify-center ${button.style}`}>
                    <button.icon className="mr-2" />
                    <div className="flex-1">{button.label}</div>
                </Button>
            ))}

            <div className="mb-6 flex w-full items-center">
                <div className="flex-grow border-t border-gray-300" />
                <span className="px-4 text-sm text-gray-500">OR</span>
                <div className="flex-grow border-t border-gray-300" />
            </div>
            <Button className="mb-4 w-full bg-[#fe2c55] text-white">Continue as guest</Button>
            <p className="mb-4 text-center text-sm text-gray-500">
                By continuing with an account located in Vietnam, you agree to our
                <a className="text-[#fe2c55]" href="#">
                    &nbsp;Terms of Service
                </a>
                &nbsp;and acknowledge that you have read our&nbsp;
                <a className="text-[#fe2c55]" href="#">
                    Privacy Policy
                </a>
                .&nbsp;
            </p>
            <p className="text-center text-sm text-black">
                Don't have an account?
                <a className="text-[#fe2c55]" href="#">
                    &nbsp;Sign up
                </a>
            </p>
        </div>
    );
}
