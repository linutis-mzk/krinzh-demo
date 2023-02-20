import { Empty, MethodKind } from "@bufbuild/protobuf";
import { HTMLResponse } from "./htmlservice_pb";
/**
 * @generated from service grpcservices.HTMLService
 */
export declare const HTMLService: {
    readonly typeName: "grpcservices.HTMLService";
    readonly methods: {
        /**
         * @generated from rpc grpcservices.HTMLService.GetIndex
         */
        readonly getIndex: {
            readonly name: "GetIndex";
            readonly I: typeof Empty;
            readonly O: typeof HTMLResponse;
            readonly kind: MethodKind.Unary;
        };
    };
};
