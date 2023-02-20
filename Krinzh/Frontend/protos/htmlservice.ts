// @generated by protobuf-ts 2.8.2
// @generated from protobuf file "htmlservice.proto" (package "grpcservices", syntax proto3)
// tslint:disable
import { Empty } from "./google/protobuf/empty";
import { ServiceType } from "@protobuf-ts/runtime-rpc";
import type { BinaryWriteOptions } from "@protobuf-ts/runtime";
import type { IBinaryWriter } from "@protobuf-ts/runtime";
import { WireType } from "@protobuf-ts/runtime";
import type { BinaryReadOptions } from "@protobuf-ts/runtime";
import type { IBinaryReader } from "@protobuf-ts/runtime";
import { UnknownFieldHandler } from "@protobuf-ts/runtime";
import type { PartialMessage } from "@protobuf-ts/runtime";
import { reflectionMergePartial } from "@protobuf-ts/runtime";
import { MESSAGE_TYPE } from "@protobuf-ts/runtime";
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
// @generated message type with reflection information, may provide speed optimized methods
class HTMLResponse$Type extends MessageType<HTMLResponse> {
    constructor() {
        super("grpcservices.HTMLResponse", [
            { no: 1, name: "content", kind: "scalar", T: 9 /*ScalarType.STRING*/ }
        ]);
    }
    create(value?: PartialMessage<HTMLResponse>): HTMLResponse {
        const message = { content: "" };
        globalThis.Object.defineProperty(message, MESSAGE_TYPE, { enumerable: false, value: this });
        if (value !== undefined)
            reflectionMergePartial<HTMLResponse>(this, message, value);
        return message;
    }
    internalBinaryRead(reader: IBinaryReader, length: number, options: BinaryReadOptions, target?: HTMLResponse): HTMLResponse {
        let message = target ?? this.create(), end = reader.pos + length;
        while (reader.pos < end) {
            let [fieldNo, wireType] = reader.tag();
            switch (fieldNo) {
                case /* string content */ 1:
                    message.content = reader.string();
                    break;
                default:
                    let u = options.readUnknownField;
                    if (u === "throw")
                        throw new globalThis.Error(`Unknown field ${fieldNo} (wire type ${wireType}) for ${this.typeName}`);
                    let d = reader.skip(wireType);
                    if (u !== false)
                        (u === true ? UnknownFieldHandler.onRead : u)(this.typeName, message, fieldNo, wireType, d);
            }
        }
        return message;
    }
    internalBinaryWrite(message: HTMLResponse, writer: IBinaryWriter, options: BinaryWriteOptions): IBinaryWriter {
        /* string content = 1; */
        if (message.content !== "")
            writer.tag(1, WireType.LengthDelimited).string(message.content);
        let u = options.writeUnknownFields;
        if (u !== false)
            (u == true ? UnknownFieldHandler.onWrite : u)(this.typeName, message, writer);
        return writer;
    }
}
/**
 * @generated MessageType for protobuf message grpcservices.HTMLResponse
 */
export const HTMLResponse = new HTMLResponse$Type();
/**
 * @generated ServiceType for protobuf service grpcservices.HTMLService
 */
export const HTMLService = new ServiceType("grpcservices.HTMLService", [
    { name: "GetIndex", options: {}, I: Empty, O: HTMLResponse }
]);
