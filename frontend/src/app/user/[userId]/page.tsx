export default ({
    params,
    ...props
}: {
    params: {
        userId: string;
    };
}) => {
    return <div>User {params.userId}</div>;
};
