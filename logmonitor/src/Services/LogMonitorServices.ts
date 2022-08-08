// import { useNavigate } from 'react-router-dom';
import axios from "axios";


class LogMonitorServices {
  private baseUrl: string = "http://localhost:5110/api";
  // private navgate = useNavigate();

  private axiosReq = axios.create({
    headers: {
      Authorization: `Bearer`,
    },
  });

  constructor() {
    this.axiosReq.interceptors.response.use(
      (res) => {
        return res;
      },
      (error) => {
        if (error.response.status === 401) {
          window.localStorage.removeItem("userToken");
          // window.location.href = "/";
          // this.navgate('/');
        }
        return Promise.reject(error);
      }
    );
  }

  getAllLogs() {
    return this.axiosReq.get(`${this.baseUrl}/LogMonitor/getAllLogs`);
  }

  getHomeDetails() {
    return this.axiosReq.get(`${this.baseUrl}/LogMonitor/getHomeDetails`);
  }

}

export default new LogMonitorServices();
