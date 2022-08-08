import React from "react";
import LogMonitorServices from "../Services/LogMonitorServices";

type HomeState = {
    data: any;
}

interface HomeProps {

}

class Home extends React.Component<HomeProps, HomeState>{
    constructor(props: HomeProps) {
        super(props);
        this.state = {
            data: {}
        };
    }

    componentDidMount() {
        LogMonitorServices.getHomeDetails().then((x) => {
            this.setState({ data: x.data });
        }, err => {
            console.log(err);
        });
    }

    render(): React.ReactNode {
        return (
            <div>
                <div>
                    <Widgets data={this.state.data} />
                </div>
            </div>
        )
    }
}

export default Home;

const Widgets = (props: any) => {
    const data = { ...props.data };
    console.log(data)
    return (
        <div style={{ display: 'flex', flexDirection: 'row', flexWrap: 'wrap', gap: '1.5rem' }}>
            {
                Object.keys(data).map((x, i) =>
                    <div key={i} style={{
                        display: 'flex',
                        padding:'0.3rem',
                        flexBasis: '25%', alignItems: 'center',
                        minHeight: '200px', background: 'white',
                        boxShadow: '0px 2px 2px 2px rgba(0,0,0,25%)', borderRadius: '0.3rem'
                    }}>
                        {data[x]}
                    </div>
                )
            }
        </div>
    )
}