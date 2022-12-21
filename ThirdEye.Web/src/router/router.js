import MessagePage from "@/pages/MessagePage.vue"
import ResetPasswordPage from "@/pages/ResetPasswordPage.vue"
import {createRouter, createWebHistory} from "vue-router"

const routes = [
    {
        path: "/message/:message/",
        component: MessagePage,
    },
    {
        path: "/reset/:email/:token",
        component: ResetPasswordPage,
    }
]

const router = createRouter({
    routes,
    history: createWebHistory()
})

export default router;