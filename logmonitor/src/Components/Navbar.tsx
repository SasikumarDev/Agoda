import React, { useRef } from "react";
import { Link, useLocation } from "react-router-dom";
import { Toolbar } from 'primereact/toolbar';
import logo from '../logo.png';
import './Navbar.css';
import globalContext from "../Context/LogMonitorContext";
import { Button } from 'primereact/button';
import { Avatar } from 'primereact/avatar';
import { Menu } from 'primereact/menu';
import { MenuItem } from "primereact/menuitem";

const Navbar = () => {
    const ctx = React.useContext(globalContext);
    const loc = useLocation();
    const menu = useRef<Menu>(null);

    if (loc.pathname === '/' && ctx?.isLoggedIn) {
        window.location.href = '/Home';
    }
    const menuItems: Array<MenuItem> = [
        { label: ctx?.LoggedUser?.Email??'' },
        {
            label: 'Logout', icon: 'pi pi-sign-out', command: () => {
                ctx?.logOut();
            }
        }];

    const leftContent = (
        <div className="leftWrapper">
            <div className="logoWrapper" >
                <img src={logo} alt="Log Monitor" style={{ width: '50px', height: '50px' }} />
                <h5 className="logoTitle" >Log Monitor</h5>
            </div>
            <div className="MenuWrapper">
                <ul className="MenuUl">
                    <li><Link to='/Home'>Home</Link></li>
                    <li><Link to='/Logs'>Logs</Link></li>
                </ul>
            </div>
        </div>
    );

    const getUserName = () => {
        var usname = '';
        if (ctx?.isLoggedIn) {
            let name: string = ctx.LoggedUser.Name;
            if (name.includes(' ')) {
                usname = name.split(' ')[0][0];
            } else {
                usname = `${name[0]}${name[1]}`;
            }
        }
        return usname;
    }

    const rightContent = (
        <div style={{ paddingRight: '0.4rem' }} >
            {
                !ctx?.isLoggedIn ? (
                    <Button label="Login" className="p-button-danger p-button-raised"></Button>
                ) : (
                    <>
                        <Avatar label={getUserName()} size="normal" shape="circle" style={{ background: 'white' }} onClick={(event) => menu?.current?.toggle?.(event)} />
                        <Menu model={menuItems} popup ref={menu} id="popup_menu" />
                    </>
                )
            }
        </div>
    );

    return (
        <>
            {
                loc.pathname !== '/' ? (<Toolbar left={leftContent} right={rightContent}>
                </Toolbar>) : null
            }
        </>
    )
}

export default Navbar;