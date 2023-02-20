import { ServiceType } from "@protobuf-ts/runtime-rpc";
import type { BinaryWriteOptions } from "@protobuf-ts/runtime";
import type { IBinaryWriter } from "@protobuf-ts/runtime";
import type { BinaryReadOptions } from "@protobuf-ts/runtime";
import type { IBinaryReader } from "@protobuf-ts/runtime";
import type { PartialMessage } from "@protobuf-ts/runtime";
import { MessageType } from "@protobuf-ts/runtime";
/**
 * @generated from protobuf message grpcservices.HTMLResponse
 */
export interface HTMLResponse {
    /**
     * @generated from protobuf field: string content = 1;
     */
    content: string;
}
declare class HTMLResponse$Type extends MessageType<HTMLResponse> {
    constructor();
    create(value?: PartialMessage<HTMLResponse>): HTMLResponse;
    internalBinaryRead(reader: IBinaryReader, length: number, options: BinaryReadOptions, target?: HTMLResponse): HTMLResponse;
    internalBinaryWrite(message: HTMLResponse, writer: IBinaryWriter, options: BinaryWriteOptions): IBinaryWriter;
}
/**
 * @generated MessageType for protobuf message grpcservices.HTMLResponse
 */
export declare const HTMLResponse: HTMLResponse$Type;
/**
 * @generated ServiceType for protobuf service grpcservices.HTMLService
 */
export declare const HTMLService: ServiceType;
export {};
