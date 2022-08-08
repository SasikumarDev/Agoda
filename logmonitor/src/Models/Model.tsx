export interface LogsResponse {
    Id: string;
    RequestType: string;
    RequestPath: string;
    RequestBody: string;
    RequestDateTime: Date;
    ResponseBody: string;
    ResponseStatus: number;
    ResponseDateTime: Date;
}