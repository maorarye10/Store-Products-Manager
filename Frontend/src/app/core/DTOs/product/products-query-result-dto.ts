import { ServerProductDTO } from "./server-product-dto";

export interface ProductsQueryResultDTO {
    products: ServerProductDTO[],
    totalCount: number
}