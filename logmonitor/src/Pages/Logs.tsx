import React from "react";
import { LogsResponse } from "../Models/Model";
import LogMonitorServices from "../Services/LogMonitorServices";
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import 'primeicons/primeicons.css';
import { PrimeIcons } from 'primereact/api';


type LogsState = {
    data: Array<LogsResponse>;
}

interface LogsProps {

}

class Logs extends React.Component<LogsProps, LogsState> {
    name: string = '';
    constructor(props: LogsProps) {
        super(props);
        this.state = {
            data: []
        };
        this.getLogData = this.getLogData.bind(this);
        this.responseStatusBody = this.responseStatusBody.bind(this);
    }

    componentDidMount() {
        console.log('Component Mounted');
        this.getLogData();
    }

    async getLogData() {
        await LogMonitorServices.getAllLogs().then((x: any) => {
            this.setState({ data: x.data as Array<LogsResponse> });
        }).catch((err) => {
            console.log(err);
        });
    }

    responseStatusBody(rowData: any) {
        let status = rowData.ResponseStatus as number;
        if (status >= 200 && status <= 299) {
            return <i style={{ color: 'green' }} className={PrimeIcons.CHECK_CIRCLE} >&nbsp;{rowData.ResponseStatus}</i>
        }
        return <i style={{ color: 'red' }} className={PrimeIcons.EXCLAMATION_TRIANGLE}>&nbsp;{rowData.ResponseStatus}</i>
    }

    render() {
        return (
            <DataTable value={this.state.data} sortMode="single" responsiveLayout="scroll" >
                <Column field="RequestType" header="Request Type" sortable></Column>
                <Column field="RequestPath" header="Request Path" sortable></Column>
                {/* <Column field="RequestBody" header="Request Body"></Column> */}
                <Column field="RequestDateTime" header="Request Time" sortable></Column>
                {/* <Column field="ResponseBody" header="Response Body"></Column> */}
                <Column field="ResponseStatus" header="Response Status" body={
                    this.responseStatusBody
                } sortable></Column>
                <Column field="ResponseDateTime" header="Response Time" sortable></Column>
            </DataTable>
        )
    }
}

export default Logs;