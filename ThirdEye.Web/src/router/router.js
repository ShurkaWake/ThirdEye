import ConfirmEmailPage from "@/pages/ConfirmEmailPage.vue"
import {createRouter, createWebHistory} from "vue-router"

const routes = [
    {
        path: "/confirm-email/:message/",
        component: ConfirmEmailPage,
    }
]

const router = createRouter({
    routes,
    history: createWebHistory()
})

export default router;