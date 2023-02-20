import type { RpcTransport } from "@protobuf-ts/runtime-rpc";
import type { ServiceInfo } from "@protobuf-ts/runtime-rpc";
import type { HTMLResponse } from "./htmlservice";
import type { Empty } from "./google/protobuf/empty";
import type { UnaryCall } from "@protobuf-ts/runtime-rpc";
import type { RpcOptions } from "@protobuf-ts/runtime-rpc";
/**
 * @generated from protobuf service grpcservices.HTMLService
 */
export interface IHTMLServiceClient {
    /**
     * @generated from protobuf rpc: GetIndex(google.protobuf.Empty) returns (grpcservices.HTMLResponse);
     */
    getIndex(input: Empty, options?: RpcOptions): UnaryCall<Empty, HTMLResponse>;
}
/**
 * @generated from protobuf service grpcservices.HTMLService
 */
export declare class HTMLServiceClient implements IHTMLServiceClient, ServiceInfo {
    private readonly _transport;
    typeName: string;
    methods: import("@protobuf-ts/runtime-rpc").MethodInfo<any, any>[];
    options: {
        [extensionName: string]: import("@protobuf-ts/runtime").JsonValue;
    };
    constructor(_transport: RpcTransport);
    /**
     * @generated from protobuf rpc: GetIndex(google.protobuf.Empty) returns (grpcservices.HTMLResponse);
     */
    getIndex(input: Empty, options?: RpcOptions): UnaryCall<Empty, HTMLResponse>;
}
