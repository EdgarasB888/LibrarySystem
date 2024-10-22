import { API_URL } from '../config/apiConfig';
import axios from "axios";

class reservationsService {
    async getAllReservations() {
        return (await axios.get(`${API_URL}/reservations`)).data;
    }

    async createReservation(reservation) {
        return (await axios.post(`${API_URL}/reservations`, reservation)).data;
    }

    async delete(reservationId) {
        return (await axios.delete(`${API_URL}/reservations/${reservationId}`)).data;
    }
}

const reservationsServiceInstance = new reservationsService();
export default reservationsServiceInstance;