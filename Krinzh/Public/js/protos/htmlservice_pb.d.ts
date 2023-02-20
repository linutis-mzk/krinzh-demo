import type { BinaryReadOptions, FieldList, JsonReadOptions, JsonValue, PartialMessage, PlainMessage } from "@bufbuild/protobuf";
import { Message } from "@bufbuild/protobuf";
/**
 * @generated from message grpcservices.HTMLResponse
 */
export declare class HTMLResponse extends Message<HTMLResponse> {
    /**
     * @generated from field: string content = 1;
     */
    content: string;
    constructor(data?: PartialMessage<HTMLResponse>);
    static readonly runtime: import("@bufbuild/protobuf/dist/types/private/proto-runtime").ProtoRuntime;
    static readonly typeName = "grpcservices.HTMLResponse";
    static readonly fields: FieldList;
    static fromBinary(bytes: Uint8Array, options?: Partial<BinaryReadOptions>): HTMLResponse;
    static fromJson(jsonValue: JsonValue, options?: Partial<JsonReadOptions>): HTMLResponse;
    static fromJsonString(jsonString: string, options?: Partial<JsonReadOptions>): HTMLResponse;
    static equals(a: HTMLResponse | PlainMessage<HTMLResponse> | undefined, b: HTMLResponse | PlainMessage<HTMLResponse> | undefined): boolean;
}
