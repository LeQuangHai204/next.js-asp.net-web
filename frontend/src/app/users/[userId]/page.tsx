import React from 'react';

interface Customer {
    id: string;
    fullName: string;
    city: string;
    nickName: string;
    address: string;
    postalCode: string;
    country: string;
}

export default async ({ params, ...props }: { params: { userId: number } }) => {
    const baseUrl: string = process.env.API || '';
    if (!baseUrl) throw new Error('API URL is not defined');

    const response: Response = await fetch(`${baseUrl}/customers/${params.userId}`);
    if (response.status == 404) throw Error('Customer not found');
    if (!response.ok) throw Error('Unknown error');
    const user: Customer = await response.json();

    return (
        <div className="bg-white text-black">
            <h1>Customer Information: </h1>
            <p>{user.id}</p>
            <p>{user.fullName}</p>
            <p>{user.city}</p>
            <p>{user.nickName}</p>
            <p>{user.address}</p>
            <p>{user.postalCode}</p>
            <p>{user.country}</p>
        </div>
    );
};
