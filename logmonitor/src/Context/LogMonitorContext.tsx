import React from "react";

type globalData = {
    isLoggedIn: boolean;
    LoggedUser: any;
    token: string;
}

type globalDataContext = {
    isLoggedIn: boolean;
    LoggedUser: any;
    token: string;
    setTokenValue(token: string): void;
    logOut(): void;
}
const initD: globalData = { isLoggedIn: false, LoggedUser: null, token: '' };
export const globalContext = React.createContext<globalDataContext | null>(null);

interface globalContextProviderProps {
    children: any;
}

class GlobalContextProvider extends React.Component<globalContextProviderProps, globalData>{
    constructor(props: globalContextProviderProps) {

        super(props);
        this.state = {
            ...initD
        };
        this.setTokenValue = this.setTokenValue.bind(this);
    }

    componentDidMount() {
        let token = window.localStorage.getItem('userToken') ?? '';
        if (token === '') {
            this.setState({ token: '', isLoggedIn: false });
        } else {
            this.setState({ token: token, isLoggedIn: true });
        }
    }

    setTokenValue(token: string) {
        window.localStorage.setItem('userToken', token);
        this.setState({ token: token, isLoggedIn: true });
    }

    LogoutUser() {
        window.localStorage.removeItem("userToken");
        window.location.href = "/";
    }

    render(): React.ReactNode {
        return (
            <globalContext.Provider value={
                {
                    isLoggedIn: this.state.isLoggedIn, LoggedUser: this.state.LoggedUser,
                    token: this.state.token, setTokenValue: this.setTokenValue, logOut: this.LogoutUser
                }}>
                {this.props.children}
            </globalContext.Provider>
        )
    }
}

export default globalContext;
export { GlobalContextProvider };