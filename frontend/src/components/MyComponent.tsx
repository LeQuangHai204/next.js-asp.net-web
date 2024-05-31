'use client';

const MyComponent = () => {
    const response = fetch('http://localhost:5058/api/v0/customers/');

    console.log(response);
    console.log('Hello World! This is the frontend of the application.');

    return <div>My Component</div>;
};

export default MyComponent;
