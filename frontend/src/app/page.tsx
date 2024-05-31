import Login from './login/page';

export default () => {
    return (
        <div className="flex min-h-screen flex-col items-center justify-center bg-gray-100 dark:bg-gray-900">
            <Login />
        </div>
    );
};

/* Component Hierarchy

<Layout>
    <Templete>
        <ErrorBoundary fallback={<Error/>}>
            <Suspense fallback={<Loading/>}>
                <ErrorBoundary fallback={<NotFound/>}>
                    <Page/>
                </ErrorBoundary>
            </Suspense>
        </ErrorBoundary>
    </Templete>
</Layout>


*/
