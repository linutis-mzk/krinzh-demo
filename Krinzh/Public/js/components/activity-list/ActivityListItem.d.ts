import { MouseEventHandler } from 'react';
interface iProps {
    position: number;
    selected: boolean;
    onClick: MouseEventHandler;
    onContextMenu: MouseEventHandler;
    description: string;
}
export declare const ActivityListItem: ({ selected, onClick, onContextMenu, description, position }: iProps) => JSX.Element;
export {};
