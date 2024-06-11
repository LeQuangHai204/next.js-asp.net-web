'use client';
import { Button } from '@/components/ui/button';

import { TriangleAlertIcon } from '@/components/icons/alert';

export default ({ error, reset }: { error: Error; reset: () => void }) => (
    <div className="flex min-h-[100dvh] flex-col items-center justify-center gap-8 px-4 py-12 text-center">
        <div className="max-w-md space-y-4">
            <TriangleAlertIcon className="mx-auto h-12 w-12 text-red-500" />
            <h1 className="text-3xl font-bold">Oops, something went wrong!</h1>
            <p className="text-gray-500 dark:text-gray-400">An unexpected ROOT error has occurred</p>
            <p className="text-gray-500 dark:text-gray-400">{error.message}</p>
            <Button
                onClick={reset}
                className="inline-flex h-10 items-center justify-center rounded-md bg-gray-900 px-6 text-sm font-medium text-gray-50 shadow transition-colors hover:bg-gray-900/90 focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-gray-950 disabled:pointer-events-none disabled:opacity-50 dark:bg-gray-50 dark:text-gray-900 dark:hover:bg-gray-50/90 dark:focus-visible:ring-gray-300"
            >
                Try Aagin
            </Button>
        </div>
    </div>
);

// error.tsx only replace page.tsx when error occurs
// the layout.tsx and other wrapper components remains interative !
